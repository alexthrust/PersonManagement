import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl, ValidatorFn } from '@angular/forms';
import { map, catchError, finalize } from 'rxjs/operators';
import { Person } from '../../model/person';
import { Gender } from '../../model/gender';
import { PersonsService } from '../../services/persons.service';
import { debug } from 'util';
import { of } from 'rxjs';

@Component({
    selector: 'app-create.dialog',
    templateUrl: './create.dialog.component.html',
    styleUrls: ['./create.dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

    personDetailsForm: FormGroup;
    genders = Gender.getGenders();

    validationMessages = {
        'firstName': [
            { type: 'required', message: 'First name is required' }
        ],
        'lastName': [
            { type: 'required', message: 'Last name is required' },
        ],
        'personalNumber': [
            { type: 'maxlength', message: 'Personal number should be 11 characters long' },
            { type: 'minlength', message: 'Personal number should be 11 characters long' },
            { type: 'pattern', message: 'Personal number should contains only numbers' },
            { type: 'validPersonalNumber', message: 'Personal number should be unique' }
        ],
        'birthdate': [
            { type: 'pattern', message: 'Birthdate should be in correct format MM/dd/yyyy' }
        ],
        'salary': [
            { type: 'pattern', message: 'Salary should be a positive number' }
        ]
    };

    constructor(public dialogRef: MatDialogRef<CreateDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Person,
        public dataService: PersonsService,
        private fb: FormBuilder) { }

    ngOnInit() {
        this.createForm();
    }

    createForm() {
        this.personDetailsForm = this.fb.group({
            firstName: [this.data != null ? this.data.firstName : '', Validators.required],
            lastName: [this.data != null ? this.data.lastName : '', Validators.required],
            // tslint:disable-next-line:max-line-length
            personalNumber: [this.data != null ? this.data.personalNumber : '', Validators.compose([Validators.minLength(11), Validators.maxLength(11), Validators.pattern('\\d+')]), this.isPersonalNumberUniqueValidator.bind(this)],
            birthdate: [this.data != null ? this.data.birthdate : ''],
            gender: new FormControl(this.data != null ? this.data.gender : this.genders[0].id),
            salary: [this.data != null ? this.data.salary : '', Validators.pattern('\\d*(\\.\\d+)?')]
        });
    }

    isPersonalNumberUniqueValidator(control: FormControl) {
        const q = new Promise((resolve, reject) => {
            setTimeout(() => {
                this.dataService.isPersonalNumberUnique(control.value, this.data != null ? this.data.id : null).subscribe((response) => {
                    return response ? resolve(null) : resolve({ 'validPersonalNumber': true });
                }, () => {
                    resolve({ 'validPersonalNumber': true });
                });
            }, 1000);
        });
        return q;
    }

    onSubmit(value: Person) {
        if (this.data == null) {
            this.dataService.createPerson(value);
        } else {
            value.id = this.data.id;
            this.dataService.updatePerson(value, this.data.id);
        }
        this.dialogRef.close(1);
    }

    onCancelClick(): void {
        this.dialogRef.close(0);
    }
}
