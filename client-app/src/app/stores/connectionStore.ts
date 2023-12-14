import { makeAutoObservable, reaction } from "mobx";
import { Connection } from "../models/connection";
import agent from "../api/agent";
import { getLocalStorageWithExpiry, removeLocalStorage, setLocalStorageWithExpiry } from "./localStorageHandler";

export default class ConnectionStore {
  connections = new Array<Connection>();
  selectedConnection: Connection | undefined = undefined;
  isLoading = false;

  constructor() {
    makeAutoObservable(this);

		const connectionJson : Connection = getLocalStorageWithExpiry('selectedConnection');
		this.selectedConnection = connectionJson;

		reaction(
			() => this.selectedConnection,
			selectedConnection => {
				if (selectedConnection) {
					setLocalStorageWithExpiry('selectedConnection', selectedConnection, 1);
				} else {
					removeLocalStorage('selectedConnection');
				}
			}
		);
  }

  private setConnection = (connection: Connection) => {
		this.connections.push(connection);
	}

  clearConnections = () => {
    this.connections.splice(0);
  }

	loadConnections = async (startStation: string, endStation: string, date: string) => {
		this.isLoading = true;
		this.clearConnections();

		try {
			const result = await agent.Connections.list(startStation, endStation, date);
			result.data.forEach((connection: Connection) => {
				this.setConnection(connection);
			})
			this.isLoading = false;
		} catch (error) {
			console.log(error);
			this.isLoading = false;
		}
	}

	loadConnection = async (id: number) => {
		this.selectedConnection = undefined;
		let connection = this.getConnection(id);
		if (connection) {
			this.selectedConnection = connection;
			return connection;
		}
	}

	private getConnection = (id: number) => {
		return this.connections.at(id);
	}
}