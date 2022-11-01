import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  // app-"component-navn"
  selector: 'app-addDeltaker',
  templateUrl: './addDeltaker.component.html'
})
export class AddDeltakerComponent {
  public nyDeltaker: AddDeltakerDTO;
  public person: AddPersonDTO;
  public http: HttpClient;
  public baseUrl: string;
  routeSub: any;
  konkurranseNavn: string = "";
  arrangementNavn: string = "";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute)
  {
    this.nyDeltaker = new AddDeltakerDTO();
    this.person = new AddPersonDTO();
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params) //log the entire params object
      console.log(params['arrangementID'] + '' + params['konkurranseID']) //log the value of id
      this.nyDeltaker.arrangementID = params['arrangementID'];
      this.nyDeltaker.konkurranseID = params['konkurranseID'];
      // navn på arr og konk
      this.konkurranseNavn = params['navn'];
      this.arrangementNavn = params['arrangementNavn'];

    });
  }
  addDeltaker() {
    console.log("add deltaker");
    alert(this.nyDeltaker.navn+" er påmeldt")

    //this.deltakerListe.personer.push(this.person);
    // baseUrl er definert i proxy.config. 'deltakelse' må ligge i context
    // Skal stemme med controller-navn
    this.http.post(this.baseUrl + 'deltakelse', this.nyDeltaker).subscribe(result => { }
      , error => console.error(error));
    this.nyDeltaker = new AddDeltakerDTO();
  }

  addPerson() {
    this.nyDeltaker.personer.push(this.person);
    this.person = new AddPersonDTO();
  }
}

export class AddDeltakerDTO {
  arrangementID: number = 1;
  konkurranseID: number = 1;
  navn: string = "";
  personer: AddPersonDTO[] = [];
}

export class AddPersonDTO {
  fornavn: string = "";
  etternavn: string = "";
  epost: string = "";
}
