import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonsDataSource } from '../services/persons.datasource';
import { PersonsService } from '../services/persons.service';
import { MatPaginator, MatSort } from '@angular/material';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    title = 'Home';

    displayedColumns = ['firstName', 'lastName', 'personalNumber', 'birthdate', 'genderName', 'salary'];
    dataSource: PersonsDataSource;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private personsService: PersonsService) { }

    ngOnInit() {
        this.dataSource = new PersonsDataSource(this.personsService);
        this.dataSource.loadPersons('', 'asc', 0, 5);
    }

}
