using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;
using Person = Toraderkonkurranse.Domene.Person;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        
        [HttpGet("meldPaaDeltaker")]
        public void opprettDeltaker([FromServices] ToraderkonkurranseContext toraderDbContext)
        {
            var faker = new Faker<Person>()
                .RuleFor(u => u.fornavn, f => f.Person.FirstName)
                .RuleFor(u => u.etternavn, f => f.Person.LastName)
                .RuleFor(u => u.epost, (f, u) => f.Internet.Email(u.fornavn, u.etternavn));
            Deltaker deltaker = new Deltaker()
            {
                navn = new Faker().Company.CompanyName()
            };
            for (int i = 0; i < 3; i++)
            {
                var f = faker.Generate();


                deltaker.leggTilPerson(new Person()
                {
                    fornavn = f.fornavn,
                    etternavn = f.etternavn,
                    epost = f.epost,
                });
                toraderDbContext.Deltakere.Add(deltaker);
            }

            toraderDbContext.SaveChanges();
        }

        

        [HttpGet("setScore")]
        public void setScore([FromServices] ToraderkonkurranseContext toraderDbContext, int konkurranseID, int deltakerID, int dommerPersonID) 
        {
            Score score = new Score()
            {
                konkurranseID = konkurranseID,
                deltakerID = deltakerID,
                konkurranseDommerID = dommerPersonID,
                arrangementScore = 1,
                formidlingScore = 2,
                taktScore = 3,
                teknikkScore = 4
            };
            toraderDbContext.Add(score);
            toraderDbContext.SaveChanges();
        }

        [HttpGet("getKonkurranseByDommer")]
        public List<int> getKonkurranseByDommer([FromServices] ToraderkonkurranseContext toraderDbContext, int dommerPersonID) 
        {
            var konkurranser = toraderDbContext.KonkurranseDommer.Where(e => e.personID == dommerPersonID).Select(e => e.konkurranseID).ToList();
            return konkurranser;

        }

        [HttpGet("getDeltakerByKonkurranse")]
        public List<Deltaker> getDeltakerByKonkurranse([FromServices] ToraderkonkurranseContext toraderDbContext, int konkurranseID)
        {
            List<int> IDdeltakere = toraderDbContext.Deltakelse.Where(e => e.konkurranseID == konkurranseID).Select(e => e.deltakerID).Distinct().ToList();
            List<Deltaker> deltakere = toraderDbContext.Deltakere.Where(e => IDdeltakere.Contains(e.deltakerID)).Include(e=> e.personer).ToList();
         
            return deltakere;
        }

        [HttpGet("OpprettDatabase")]
        public void opprettDatabase([FromServices] ToraderkonkurranseContext toraderDbContext)
        {
            //Tømmer database
            toraderDbContext?.Database.EnsureDeleted();

            //Oppretter database
            toraderDbContext?.Database.EnsureCreated();
        }

        [HttpGet("getAlleResultaterPerDeltaker")]
        public string getAlleResultatPerDeltaker([FromServices] ToraderkonkurranseContext context, int deltakerID)
        {
            var deltakerNavn = context.Deltakere.Where(d => d.deltakerID == deltakerID).Select(d => d.navn).First();
            // alle deltakelser per deltaker
            var konkArrListe = context.Deltakelse
                .Where(deltagelse => deltagelse.deltakerID == deltakerID)
                .Include(deltagelse => deltagelse.arrangement)
                .Select(deltagelse =>
                new
                {
                    konkurranseID = deltagelse.konkurranseID,
                    konkurranseNavn = context.Konkurranser.Where(konk => konk.konkurranseID == deltagelse.konkurranseID).FirstOrDefault().navn,
                    arrangementNavn = deltagelse.arrangement.navn,
                }).Distinct().ToList();


            var resultatString = konkArrListe
                .Select(konk => new
                {
                    arrangementNavn = konk.arrangementNavn,
                    konkurranse = new
                    {
                        konk.konkurranseNavn,
                        score = context.Score.Where(score => score.konkurranseID == konk.konkurranseID && score.deltakerID == deltakerID).ToList().Select(score => score.getSamletScore()).Sum()
                    }
                }).ToList();

            return JsonSerializer.Serialize(resultatString);
        }
    }
}
