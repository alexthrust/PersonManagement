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

    getAllPersons(filter, sortField, sortDesc, pageNumber = 0, pageSize = 5): Observable<CustomStoreDataModel<Person>> {
        let params = new HttpParams()
            .set('filterValue', filter)
            .set('skip', (pageNumber * pageSize).toString())
            .set('take', pageSize.toString());

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
}
