import { Component, AfterViewInit, OnInit, ElementRef, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { merge, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { PersonsDataSource } from '../services/persons.datasource';
import { PersonsService } from '../services/persons.service';
import { Person } from '../model/person';
import { CreateDialogComponent } from '../dialogs/create.dialog/create.dialog.component';
import { DeleteDialogComponent } from '../dialogs/delete.dialog/delete.dialog.component';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {

    title = 'Home';

    displayedColumns = ['firstName', 'lastName', 'personalNumber', 'birthdate', 'gender', 'salary', 'actions'];
    dataSource: PersonsDataSource;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    @ViewChild('filter') filter: ElementRef;

    constructor(private personsService: PersonsService,
        public dialog: MatDialog) { }

    ngOnInit() {
        this.dataSource = new PersonsDataSource(this.personsService);
        this.dataSource.loadPersons(null, null, null, 0, 10);
    }

    ngAfterViewInit() {

        this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

        fromEvent(this.filter.nativeElement, 'keyup')
            .pipe(
                debounceTime(150),
                distinctUntilChanged(),
                tap(() => {
                    this.paginator.pageIndex = 0;

                    this.loadPersonsPage();
                })
            )
            .subscribe();

        merge(this.sort.sortChange, this.paginator.page)
            .pipe(
                tap(() => this.loadPersonsPage())
            )
            .subscribe();

    }

    loadPersonsPage() {
        this.dataSource.loadPersons(
            this.filter.nativeElement.value,
            this.sort.active,
            this.sort.direction,
            this.paginator.pageIndex,
            this.paginator.pageSize);
    }

    createPerson(person: Person) {
        const dialogRef = this.dialog.open(CreateDialogComponent, { data: null });

        dialogRef.afterClosed().subscribe(result => {
            if (result === 1) {
                this.loadPersonsPage();
            }
        });
    }

    updatePerson(rowIndex: number, row: Person) {
        const dialogRef = this.dialog.open(CreateDialogComponent, { data: row });

        dialogRef.afterClosed().subscribe(result => {
            if (result === 1) {
                this.loadPersonsPage();
            }
        });
    }

    deletePerson(rowIndex: number, row: Person) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, { data: row });

        dialogRef.afterClosed().subscribe(result => {
            if (result === 1) {
                this.loadPersonsPage();
            }
        });
    }
}
