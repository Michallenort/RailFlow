import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useState } from "react";

export default observer(function SignUpForm() {
  
  const { userStore } = useStore();

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(Date());
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [nationality, setNationality] = useState('');

  const onSubmit = (e: any) => {
    e.preventDefault();
    userStore.signUp({
      firstName: firstName, 
      lastName: lastName, 
      email: email, 
      dateOfBirth: new Date(dateOfBirth), 
      password: password, 
      confirmPassword: confirmPassword,
      nationality: nationality
    });
  }
  
  return (
    <div className="container">
          <div className="row justify-content-center">
            <div className="col-xl-5 col-md-8">
              <form className="bg-white rounded shadow-5-strong p-5">
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="firstName">First Name</label>
                  <input 
                  type="text" 
                  id="firstName" 
                  className="form-control" 
                  required 
                  onChange={e => setFirstName(e.target.value)}
                  value={firstName}
                  />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="lastName">Last Name</label>
                  <input 
                  type="text" 
                  id="lastName" 
                  className="form-control" 
                  required 
                  onChange={e => setLastName(e.target.value)}
                  value={lastName}
                  />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="email">Email address</label>
                  <input 
                  type="email" 
                  id="email" 
                  className="form-control" 
                  required 
                  onChange={e => setEmail(e.target.value)}
                  value={email}
                  />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="dateOfBirth">Date of Birth</label>
                  <input 
                  type="date" 
                  id="dateOfBirth" 
                  className="form-control" 
                  required 
                  onChange={e => setDateOfBirth(e.target.value)}
                  value={dateOfBirth}
                  />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="password">Password: </label>
                  <input 
                    type="password" 
                    id="password" 
                    className="form-control" 
                    required
                    onChange={e => setPassword(e.target.value)}
                    value={password}
                    />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="confirmPassword">Confirm Password: </label>
                  <input 
                    type="password" 
                    id="confirmPassword" 
                    className="form-control" 
                    required
                    onChange={e => setConfirmPassword(e.target.value)}
                    value={confirmPassword}
                    />
                </div>
                <button type="submit" className="btn btn-primary btn-block" onClick={e => onSubmit(e)}>Sign in</button>
              </form>
            </div>
          </div>
        </div>
  )
})