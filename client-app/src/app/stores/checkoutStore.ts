import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { ClientSecret, Config, PaymentIntent } from "../models/checkout";



export default class CheckoutStore {
  config: Config | null = null;
  clientSecret: ClientSecret | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  clearConfig = () => {
    this.config = null;
  }

  clearClientSecret = () => {
    this.clientSecret = null;
  }

  loadConfig = async () => {
    try {
      this.clearConfig();
      const response = await agent.Checkouts.config();
      this.config = response.data;
      return response.data;
    } catch(error) {
      console.log(error);
    }
  }

  loadClientSecret = async (intent: PaymentIntent) => {
    try {
      this.clearClientSecret();
      const response = await agent.Checkouts.createPaymentIntent(intent);
      this.clientSecret = response.data;
      return response.data;
    } catch(error) {
      console.log(error);
    }
  }
}