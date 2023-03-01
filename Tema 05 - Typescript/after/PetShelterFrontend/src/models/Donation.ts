import type { Person } from './Person';
export default class Donation{
    amount:number;
    donor:Person;
   
    constructor(amount:number,donor:Person) {
        this.amount=amount;
        this.donor=donor;
        
    }
}