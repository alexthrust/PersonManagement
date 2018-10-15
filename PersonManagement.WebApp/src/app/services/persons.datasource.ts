import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';

import { Person } from '../model/person';
import { PersonsService } from './persons.service';
import { CustomStoreDataModel } from '../model/custom.store.data.model';


export class PersonsDataSource implements DataSource<Person> {

    private personsSubject = new BehaviorSubject<Person[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();
    public totalItems = 0;

    constructor(private personsService: PersonsService) {

    }

    loadPersons(filter: string,
        sortField: string,
        sortDesc: string,
        pageIndex: number,
        pageSize: number) {

        this.loadingSubject.next(true);

        this.personsService.getAllPersons(filter, sortField, sortDesc, pageIndex, pageSize)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)),
                map((response: CustomStoreDataModel<Person>) => {
                    this.totalItems = response.totalItems;
                    return response.items;
                })
            )
            .subscribe(persons => this.personsSubject.next(persons));
    }

    connect(collectionViewer: CollectionViewer): Observable<Person[]> {
        console.log('Connecting data source');
        return this.personsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.personsSubject.complete();
        this.loadingSubject.complete();
    }
}
