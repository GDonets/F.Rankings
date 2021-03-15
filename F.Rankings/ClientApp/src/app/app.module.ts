import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatInputModule } from "@angular/material/input";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AgentRanksTableComponent } from './agent-ranks-table/agent-ranks-table.component';
import { RankingsService } from './services/rankings-service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AgentRanksTableComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'rankings', component: AgentRanksTableComponent },
      { path: 'rankings/:gardenFlag', component: AgentRanksTableComponent },
    ], { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule
  ],
  providers: [ RankingsService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
