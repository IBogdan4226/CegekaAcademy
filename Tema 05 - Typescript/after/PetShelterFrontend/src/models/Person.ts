export class Person {
    name: string;
    idNumber: string;
    dateOfBirth:Date;
    constructor(name: string, id: string,birth:Date) {
        this.name = name;
        this.idNumber = id;
        this.dateOfBirth=birth;
    }
}