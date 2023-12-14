import { useEffect } from "react";
import { useStore } from "../../app/stores/store"

export default function Completion() {
  const {userStore, connectionStore, reservationStore} = useStore();
  const {selectedConnection} = connectionStore;
  const {createReservation} = reservationStore;
  useEffect(() => {
    console.log(selectedConnection?.subConnections[0].schedule.date!);
    console.log(userStore.loggedUser?.id!);
    console.log(selectedConnection?.subConnections[0].schedule.id!);
    console.log(selectedConnection?.subConnections.length === 2 ? selectedConnection?.subConnections[1].schedule.id! : undefined);
    console.log(selectedConnection?.startStopId!);
    console.log(selectedConnection?.startHour!);
    console.log(selectedConnection?.endStopId!);
    console.log(selectedConnection?.endHour!);
    console.log(selectedConnection?.price!);

    createReservation({
      date: selectedConnection?.subConnections[0].schedule.date!,
      userId: userStore.loggedUser?.id!,
      firstScheduleId: selectedConnection?.subConnections[0].schedule.id!,
      secondScheduleId: selectedConnection?.subConnections.length === 2 ? selectedConnection?.subConnections[1].schedule.id! : undefined,
      startStopId: selectedConnection?.startStopId!,
      startHour: selectedConnection?.startHour!,
      endStopId: selectedConnection?.endStopId!,
      endHour: selectedConnection?.endHour!,
      price: selectedConnection?.price!,
    
    });
  }, []);


  return (
    <div>
      <h1>Payment succesfully completetd</h1>
    </div>
  )
}