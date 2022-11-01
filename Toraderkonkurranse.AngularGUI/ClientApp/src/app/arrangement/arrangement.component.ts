import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  // app-"component-navn"
  selector: 'app-arrangement',
  templateUrl: './arrangement.component.html'
})
export class ArrangementComponent {
  public arrangement: AddArrangementDTO;
  public http: HttpClient;
  public baseUrl: string;
  public arrangementID: number;
  routeSub: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.arrangement = new AddArrangementDTO();
    this.http = http;
    this.baseUrl = baseUrl;
    this.arrangementID = 0;
  }

  opprettArrangement() {
    this.http.post(this.baseUrl + 'arrangement', this.arrangement).subscribe(result => { }
      , error => console.error(error));
    this.arrangement = new AddArrangementDTO();
  }

    //this.deltakerListe.personer.push(this.person);
    // baseUrl er definert i proxy.config. 'deltakelse' må ligge i context
    // Skal stemme med controller-navn

  //Het er et eksempel på hvordan sende noen parametere(prosjektId, uttrekksdefinisjonId) som del av Url....

  //Angular:

  //settKriterieNokkelfilliste(prosjektId: number, uttrekksdefinisjonId: number, kriterier: KriterieNokkelfilIUttrekk[]) {
  //  const apiUrl = `${this.apiRoot}api/individdatauttrekk/prosjekter/${prosjektId}/uttrekksdefinisjoner/${uttrekksdefinisjonId}/kriterieNokkelfiler`;
  //  return this._http.put<string[]>(apiUrl, kriterier).toPromise();
  //}

  //  C#

  //  [HttpPut("{prosjektId}/uttrekksdefinisjoner/{uttrekksdefinisjonId}/kriterieNokkelfilliste")]
  //  public ActionResult SettKriterieNokkelfilliste(int prosjektId, int uttrekksdefinisjonId, [FromBody] List<KriterieNøkkelfilDto> kiterieNøkkelfilliste)
  //{
  //  var fantProsjektet = _prosjektTjeneste.SettKriterieNøkkelfilliste(prosjektId, uttrekksdefinisjonId, kiterieNøkkelfilliste);
  //  if (!fantProsjektet) return NotFound($"Fant ikke prosjekt med id {prosjektId}");
  //  return Ok();
  //}

}

export class AddArrangementDTO {
  arrangor: string = "";
  navn: string = "";
  lokasjon: string = "";
  startDato: Date = new Date();
  sluttDato: Date = new Date();
}

