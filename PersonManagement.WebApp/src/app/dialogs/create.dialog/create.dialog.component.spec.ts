import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Create.DialogComponent } from './create.dialog.component';

describe('Create.DialogComponent', () => {
  let component: Create.DialogComponent;
  let fixture: ComponentFixture<Create.DialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Create.DialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Create.DialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
