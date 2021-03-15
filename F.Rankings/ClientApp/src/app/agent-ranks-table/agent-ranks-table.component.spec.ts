import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AgentRanksTableComponent } from './agent-ranks-table.component';

describe('AgentRanksTableComponent', () => {
  let component: AgentRanksTableComponent;
  let fixture: ComponentFixture<AgentRanksTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentRanksTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentRanksTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
