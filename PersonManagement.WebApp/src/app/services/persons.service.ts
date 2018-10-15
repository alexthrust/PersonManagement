import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Person } from '../model/person';
import { environment } from '../../environments/environment';
import { CustomStoreDataModel } from '../model/custom.store.data.model';
import { MatSnackBar } from '../../../node_modules/@angular/material';


@Injectable()
export class PersonsService {
    // Temporarily stores data from dialogs
    dialogData: any;

    constructor(private httpClient: HttpClient,
        public snackBar: MatSnackBar) { }

    getDialogData() {
        return this.dialogData;
    }

    getAllPersons(filter, sortField, sortDesc, pageNumber = 0, pageSize = 5): Observable<CustomStoreDataModel<Person>> {
        let params = new HttpParams()
            .set('skip', (pageNumber * pageSize).toString())
            .set('take', pageSize.toString());

        if (filter != null && filter !== '') {
            params = params.append('filterValue', filter);
        }

        if (sortDesc != null && sortDesc !== '') {
            params = params.append('sort.Desc', (sortDesc === 'desc').toString());
        }

        if (sortField != null) {
            params = params.append('sort.Field', sortField);
        }

        return this.httpClient.get(`${environment.apiUrl}/person/records`, {
            params: params
        }).pipe(
            map((response: CustomStoreDataModel<Person>) => response)
        );
    }

    isPersonalNumberUnique(value: any, personId: number): Observable<boolean> {
        let params = new HttpParams()
            .set('personalNumber', value);

        if (personId != null) {
            params = params.append('personId', personId.toString());
        }

        return this.httpClient.get(`${environment.apiUrl}/person/unique`, {
            params: params
        }).pipe(
            map((response: boolean) => response)
        );
    }

    createPerson(person: Person): void {
        this.httpClient.post(`${environment.apiUrl}/person`, person).subscribe(data => {
            this.snackBar.open('Successfully created', null, { duration: 3000 });
        },
            (err: HttpErrorResponse) => {
                this.snackBar.open('Error occurred', null, { duration: 8000 });
            });
    }

    updatePerson(person: Person, personId: number): void {
        this.httpClient.put(`${environment.apiUrl}/person/${personId}`, person).subscribe(data => {
            this.snackBar.open('Successfully updated', null, { duration: 3000 });
        },
            (err: HttpErrorResponse) => {
                this.snackBar.open('Error occurred', null, { duration: 8000 });
            });
    }

    deletePerson(personId: number): void {
        this.httpClient.delete(`${environment.apiUrl}/person/${personId}`).subscribe(data => {
            this.snackBar.open('Successfully deleted', null, { duration: 3000 });
        },
            (err: HttpErrorResponse) => {
                this.snackBar.open('Error occurred', null, { duration: 8000 });
            });
    }
}
