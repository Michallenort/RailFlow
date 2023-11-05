import { observer } from "mobx-react-lite";
import { useState } from "react";

export default observer(function CreateStation() {
  
  const [name, setName] = useState('');
  const [country, setCountry] = useState('');
  const [city, setCity] = useState('');
  const [street, setStreet] = useState('');
  
  return (
    <div className="container mt-5"></div>
  )
})