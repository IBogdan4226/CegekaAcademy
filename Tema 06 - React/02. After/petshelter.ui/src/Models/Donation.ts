import type { RealPerson } from './RealPerson';
export default class Donation{
    amount:number;
    donor:RealPerson;
   
    constructor(amount:number,donor:RealPerson) {
        this.amount=amount;
        this.donor=donor;
        
    }
}