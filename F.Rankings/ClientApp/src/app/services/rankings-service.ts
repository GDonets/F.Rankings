import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { RankedAgent } from '../models/agent';

@Injectable()
export class RankingsService {
  constructor(private http: HttpClient) { }

  public getAgentsRankedByPropertyWithGarden(count: number) {
    var url = environment.apiBaseUrl + '/rankings/topagentswithgarden/' + (count > 0 ? count : 1);
    return this.http.get<RankedAgent[]>(url).pipe(catchError(this.handleError));
  }

  public getAgentsRankedByPropertyWithoutGarden(count: number) {
    var url = environment.apiBaseUrl + '/rankings/topagentsnogarden/' + (count > 0 ? count : 1);
    return this.http.get<RankedAgent[]>(url).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}
