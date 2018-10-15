import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Person } from '../model/person';
import { environment } from '../../environments/environment';
import { CustomStoreDataModel } from '../model/custom.store.data.model';


@Injectable()
export class PersonsService {

    // Temporarily stores data from dialogs
    dialogData: any;

    constructor(private httpClient: HttpClient) { }

    getDialogData() {
        return this.dialogData;
    }

    getAllPersons(filter = '', sortOrder = 'asc', pageNumber = 0, pageSize = 5): Observable<CustomStoreDataModel<Person>> {
        return this.httpClient.get(`${environment.apiUrl}/person/records`, {
            params: new HttpParams()
                .set('filterValue', filter)
                .set('sortOrder', sortOrder)
                .set('skip', pageNumber.toString())
                .set('take', pageSize.toString())
        }).pipe(
            map((response: CustomStoreDataModel<Person>) => response)
        );
    }
}
