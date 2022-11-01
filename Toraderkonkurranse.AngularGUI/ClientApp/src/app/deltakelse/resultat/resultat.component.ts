import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  // app-"component-navn"
  selector: 'app-resultat',
  templateUrl: './resultat.component.html'
})
export class ResultatComponent {
  public resultatListe: any;
  routeSub: any;
  konkurranseID: number = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
   
  }
  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params) //log the entire params object
      console.log(params['konkurranseID']) //log the value of id
      this.konkurranseID = params['konkurranseID'];
      this.http.get<string>(this.baseUrl + 'resultat?konkurranseID=' + params['konkurranseID']).subscribe(result => {
        this.resultatListe = result;
      }, error => console.error(error));
    });
  }
}

