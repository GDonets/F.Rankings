import { RankedAgent } from "../models/agent";
import { Observable, BehaviorSubject } from "rxjs";
import { CollectionViewer } from "@angular/cdk/collections";
import { RankingsService } from "../services/rankings-service";
import { finalize } from "rxjs/operators";
import { RankedAgentDataSourceBase } from "./ranked-agent-datasource-base";


export class RankedAgentNoGardenDataSource implements RankedAgentDataSourceBase {

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
  
    loadAgentsRanks(count: number) {
      this.loadingSubject.next(true);

      this.dataService
        .getAgentsRankedByPropertyWithoutGarden(count)
        .pipe(finalize(() => this.loadingSubject.next(false)))
        .subscribe(agents => this.agentsSubject.next(agents));
    }  
}
