export class Gender {
    private static genderNames = [{ id: 0, name: '' }, { id: 1, name: 'Male' }, { id: 2, name: 'Female' }];

    public static getGenders() {
        return this.genderNames;
    }

    public static getGenderNameById(id: number) {
        return this.genderNames.filter(gen => gen.id === id)[0].name;
    }
}
