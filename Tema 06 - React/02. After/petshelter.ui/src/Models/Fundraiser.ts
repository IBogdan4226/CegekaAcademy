import type { RealPerson } from './RealPerson';

export class Fundraiser {
  id?:number;
  name: string;
  target: number;
  currentlyRaised?: number;
  dueDate: Date;
  creationDate?: Date;
  status?: FundraiserStatus;
  owner: RealPerson;
  donors?: RealPerson[];

  constructor(
    name: string,
    target: number,
    dueDate: Date,
    owner: RealPerson,
    id?:number,
    creationDate?: Date,
    status?: FundraiserStatus,
    currentlyRaised?: number,
    donors?: RealPerson[],
  ) {
    this.name = name;
    this.target = target;
    this.currentlyRaised = currentlyRaised;
    this.creationDate = creationDate;
    this.dueDate = dueDate;
    this.status = status;
    this.owner = owner;
    this.donors = donors;
    this.id=id;
  }
}

export const enum FundraiserStatus {
  'Closed' = 'Closed',
  'Active' = 'Active',
}