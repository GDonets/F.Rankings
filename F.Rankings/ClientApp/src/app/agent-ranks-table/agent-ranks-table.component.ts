import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { RankedAgentGardenDataSource } from '../sources/ranked-agent-garden-datasource';
import { RankedAgentDataSourceBase } from '../sources/ranked-agent-datasource-base';
import { RankingsService } from '../services/rankings-service';
import { RankedAgentNoGardenDataSource } from '../sources/ranked-agent-nogarden-datasource';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'agent-ranks-table',
  templateUrl: './agent-ranks-table.component.html',
  styleUrls: ['./agent-ranks-table.component.css']
})
export class AgentRanksTableComponent implements OnInit, OnDestroy {
  private paramSubscription: Subscription;

  dataSource: RankedAgentDataSourceBase;
  displayedColumns = ["rank", "name", "objectsCount"];

  constructor(private service: RankingsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.paramSubscription = this.route.params.subscribe(el => {
      var withGarden = el.gardenFlag === 'withgarden';

      this.dataSource = withGarden
        ? new RankedAgentGardenDataSource(this.service)
        : new RankedAgentNoGardenDataSource(this.service);

      this.dataSource.loadAgentsRanks(10);
    });
  }

  ngOnDestroy(): void {
      this.paramSubscription.unsubscribe();
  }
}
