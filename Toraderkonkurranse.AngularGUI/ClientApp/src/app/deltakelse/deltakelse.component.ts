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
  erPlanlagt(status: Status) {
    if (status.includes('planlagt')) {
      return true;
    }
    return false;
  }


}

interface GetArrangement {
  arrangementID: number,
  arrangor: string,
  navn: string,
  lokasjon: string,
  status: Status
}

export enum Status
{
  planlagt = 'planlagt',
  aktiv = 'aktiv',
  avsluttet = 'avsluttet',
  avlyst = 'avlyst'
}
