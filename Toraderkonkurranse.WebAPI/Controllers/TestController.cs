using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;
using Person = Toraderkonkurranse.Domene.Person;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("OpprettArrangement")]
        public void opprettArrangement([FromServices] ToraderkonkurranseContext toraderDbContext)
        {
            Konkurranse konk1 = new Konkurranse()
            {
                navn = "Grupper",
                tidspunkt = DateTime.Now.AddDays(2),
                maxAntallDeltakere = 15,
            };

            Konkurranse konk2 = new Konkurranse()
            {
                navn = "Solist",
                tidspunkt = DateTime.Now.AddDays(1),
                maxAntallDeltakere = 1
            };

            Arrangement arr = new Arrangement()
            {
                status = Status.planlagt,
                arrangor = "Osterøybelgen",
                lokasjon = "Osterøy",
                navn = "Vestlandsmesterskap 2022",
            };

            arr.leggTilKonkurranse(konk1);
            arr.leggTilKonkurranse(konk2);

            Person dommer1 = new Person()
            {
                fornavn = "Dommer1",
                erDommer = true,
            };
            toraderDbContext.Personer.Add(dommer1);

            Person dommer2 = new Person()
            {
                fornavn = "Dommer2",
                erDommer = true
            };
            toraderDbContext.Arrangement.Add(arr);
            toraderDbContext.Personer.Add(dommer2);
            toraderDbContext.SaveChanges();

            foreach (var konkurranse in arr.konkurranseliste)
            {
                KonkurranseDommer konkurranseDommer1 = new KonkurranseDommer()
                {
                    personID = dommer1.personID,
                    konkurranseID = konkurranse.konkurranseID
                };
                toraderDbContext.KonkurranseDommer.Add(konkurranseDommer1);
                KonkurranseDommer konkurranseDommer2 = new KonkurranseDommer()
                {
                    personID = dommer2.personID,
                    konkurranseID = konkurranse.konkurranseID
                };
                toraderDbContext.KonkurranseDommer.Add(konkurranseDommer2);
            }
            toraderDbContext.SaveChanges();
        }

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

        [HttpGet("paameldeDeltaker")]
        public void paameldeDeltaker([FromServices] ToraderkonkurranseContext toraderDbContext,int arrangementID, int konkurranseID, int deltakerID)
        {
            var arrangement = toraderDbContext.Arrangement.Where(e => e.arrangementID == arrangementID).FirstOrDefault();
            var deltaker = toraderDbContext.Deltakere.Where(e => e.deltakerID == deltakerID).Include(e => e.personer).FirstOrDefault();
            arrangement.meldPaaDetaker(deltaker, konkurranseID);
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

    }
}
