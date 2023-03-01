import type { Person } from './Person';

export class Fundraiser {
  name: string;
  target: number;
  currentlyRaised?: number;
  dueDate: Date;
  creationDate?: Date;
  status?: FundraiserStatus;
  owner: Person;
  donors?: Person[];

  constructor(
    name: string,
    target: number,
    dueDate: Date,
    owner: Person,
    creationDate?: Date,
    status?: FundraiserStatus,
    currentlyRaised?: number,
    donors?: Person[],
  ) {
    this.name = name;
    this.target = target;
    this.currentlyRaised = currentlyRaised;
    this.creationDate = creationDate;
    this.dueDate = dueDate;
    this.status = status;
    this.owner = owner;
    this.donors = donors;
  }
}

export const enum FundraiserStatus {
  'Closed' = 0,
  'Active' = 1,
}
