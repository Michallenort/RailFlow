export interface Station {
  id: string;
  name: string;
  country: string;
  city: string;
  street: string;
}

export interface StationFormValues {
  name: string;
  country: string;
  city: string;
  street: string;
}

export class Station implements Station {
  constructor(init?: StationFormValues) {
    Object.assign(this, init);
  }
}