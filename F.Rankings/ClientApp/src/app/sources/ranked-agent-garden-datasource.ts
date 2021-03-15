import { RankedAgent } from "../models/agent";
import { Observable, BehaviorSubject } from "rxjs";
import { DataSource } from "@angular/cdk/table";
import { CollectionViewer } from "@angular/cdk/collections";
import { RankingsService } from "../services/rankings-service";
import { finalize } from "rxjs/operators";
import { RankedAgentDataSourceBase } from "./ranked-agent-datasource-base";


export class RankedAgentGardenDataSource implements RankedAgentDataSourceBase {

  private agentsSubject = new BehaviorSubject<RankedAgent[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.loadingSubject.asObservable();

  constructor(private dataService: RankingsService) {}

  connect(collectionViewer: CollectionViewer): Observable<RankedAgent[]> {
    return this.agentsSubject.asObservable();
  }

    disconnect(collectionViewer: CollectionViewer): void {
      this.agentsSubject.complete();
      this.agentsSubject.complete();
    }
  
    loadAgentsRanks(count: number): void {
      this.loadingSubject.next(true);

      this.dataService
        .getAgentsRankedByPropertyWithGarden(count)
        .pipe(finalize(() => this.loadingSubject.next(false)))
        .subscribe(agents => this.agentsSubject.next(agents));
    }  
}
