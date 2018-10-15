import { Component, AfterViewInit, OnInit, ElementRef, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { merge, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { PersonsDataSource } from '../services/persons.datasource';
import { PersonsService } from '../services/persons.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {

    title = 'Home';

    displayedColumns = ['firstName', 'lastName', 'personalNumber', 'birthdate', 'genderName', 'salary'];
    dataSource: PersonsDataSource;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    @ViewChild('filter') filter: ElementRef;

    constructor(private personsService: PersonsService) { }

    ngOnInit() {
        this.dataSource = new PersonsDataSource(this.personsService);
        this.dataSource.loadPersons('', this.displayedColumns[0], '', 0, 10);
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
}
