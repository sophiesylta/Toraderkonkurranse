import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  // app-"component-navn"
  selector: 'app-dommer',
  templateUrl: './dommer.component.html'
})
export class DommerComponent {
  public person: AddPersonDTO;
  public http: HttpClient;
  public baseUrl: string;
  public konkurranseID: number;
  routeSub: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.person = new AddPersonDTO();
    this.http = http;
    this.baseUrl = baseUrl;
    this.konkurranseID = 0;
  }

  opprettDommer() {
    let dommer = new AddDommerDTO();
    dommer.person = this.person;
    dommer.konkurranseID = this.konkurranseID;
    this.http.post(this.baseUrl + 'dommer', dommer).subscribe(result => { }
      , error => console.error(error));
    this.person = new AddPersonDTO();
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

export class AddPersonDTO {
  fornavn: string = "";
  etternavn: string = "";
  epost: string = "";
}

export class AddDommerDTO {
  person: AddPersonDTO = new AddPersonDTO();
  konkurranseID: number = 0;
}

