import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';
import { Person } from '../../model/person';
import { PersonsService } from '../../services/persons.service';

@Component({
    selector: 'app-delete.dialog',
    templateUrl: './delete.dialog.component.html',
    styleUrls: ['./delete.dialog.component.css']
})
export class DeleteDialogComponent implements OnInit {

    constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Person,
        public dataService: PersonsService) { }

    ngOnInit() {
    }

    onDeleteClick() {
        this.dataService.deletePerson(this.data.id);
        this.dialogRef.close(1);
    }

    onCancelClick(): void {
        this.dialogRef.close(0);
    }
}
