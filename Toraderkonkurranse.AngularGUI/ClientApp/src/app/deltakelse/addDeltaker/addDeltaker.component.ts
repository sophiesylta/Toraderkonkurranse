import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  // app-"component-navn"
  selector: 'app-addDeltaker',
  templateUrl: './addDeltaker.component.html'
})
export class AddDeltakerComponent {
  public deltaker: AddDeltakerDTO;
  public http: HttpClient;
  public baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.deltaker = new AddDeltakerDTO();
    this.http = http;
    this.baseUrl = baseUrl;
  }
  addDeltaker() {
    console.log("add deltaker");
        // baseUrl er definert i proxy.config. 'addDeltaker' må ligge i context
    this.http.post(this.baseUrl + 'deltakelse/addDeltaker', this.deltaker).subscribe(result => { }
      , error => console.error(error));
  }
}

export class AddDeltakerDTO {
  navn: string = "";
  personer: AddPersonDTO = new AddPersonDTO;
}

export class AddPersonDTO {
  fornavn: string = "";
  etternavn: string = "";
  epost: string = "";
}
