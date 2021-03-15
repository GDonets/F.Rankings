import { RankedAgent } from "../models/agent";
import { BehaviorSubject, Observable } from "rxjs";
import { DataSource } from "@angular/cdk/table";


export abstract class RankedAgentDataSourceBase extends DataSource<RankedAgent> {
  public loading$: Observable<boolean>
  abstract loadAgentsRanks(count: number): void
}
