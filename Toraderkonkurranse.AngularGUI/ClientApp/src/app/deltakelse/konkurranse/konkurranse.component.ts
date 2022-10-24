import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  // app-"component-navn"
  selector: 'app-konkurranse',
  templateUrl: './konkurranse.component.html'
})
export class KonkurranseComponent {
  public konkurranse: Konkurranse[] = [];
    routeSub: any;
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
   
  }
  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params) //log the entire params object
      console.log(params['id']) //log the value of id
      this.http.get<Konkurranse[]>(this.baseUrl + 'konkurranse?arrangementID=' + params['id']).subscribe(result => {
        this.konkurranse = result;
      }, error => console.error(error));
    });
  }
}

interface Konkurranse {
  konkurranseID: number,
  navn: string,
  maxAntallDeltakere: number,
}
