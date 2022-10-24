import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  // app-"component-navn"
  selector: 'app-deltakelse',
  templateUrl: './deltakelse.component.html'
})
export class DeltakelseComponent {
  public arrangement: GetArrangement[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<GetArrangement[]>(baseUrl + 'deltakelse').subscribe(result => {
      this.arrangement = result;
    }, error => console.error(error));
  }
}

interface GetArrangement {
    arrangementID: number,
    arrangor: string,
    navn: string,
    lokasjon:string
}
