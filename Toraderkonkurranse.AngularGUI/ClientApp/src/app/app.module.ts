import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DeltakelseComponent } from './deltakelse/deltakelse.component';
import { KonkurranseComponent } from './deltakelse/konkurranse/konkurranse.component';
import { AddDeltakerComponent } from './deltakelse/addDeltaker/addDeltaker.component';
import { ResultatComponent } from './deltakelse/resultat/resultat.component';
import { DommerComponent } from './dommer/dommer.component';
import { ArrangementComponent } from './arrangement/arrangement.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DeltakelseComponent,
    KonkurranseComponent,
    AddDeltakerComponent,
    ResultatComponent,
    DommerComponent,
    ArrangementComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'deltakelse', component: DeltakelseComponent },
      // path for nettleseren mellom sidene
      { path: 'konkurranse/:id/:navn/:status', component: KonkurranseComponent },
      { path: 'addDeltaker/:arrangementID/:konkurranseID/:navn/:arrangementNavn', component: AddDeltakerComponent },
      { path: 'resultat/:konkurranseID', component: ResultatComponent },
      { path: 'dommer', component: DommerComponent },
      { path: 'arrangement', component: ArrangementComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
