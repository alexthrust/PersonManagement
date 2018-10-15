import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Delete.DialogComponent } from './delete.dialog.component';

describe('Delete.DialogComponent', () => {
  let component: Delete.DialogComponent;
  let fixture: ComponentFixture<Delete.DialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Delete.DialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Delete.DialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
