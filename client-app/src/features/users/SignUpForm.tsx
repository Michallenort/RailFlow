import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useState } from "react";
import { router } from "../../app/router/Routes";
import { SignUpFormValues } from "../../app/models/user";
import agent from "../../app/api/agent";

export default observer(function SignUpForm() {
  
  const { userStore } = useStore();
  const { signUp } = userStore;

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(Date());
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [nationality, setNationality] = useState('');

  const [displayWarning, setDisplayWarning] = useState(false);
	const [displaySuccess, setDisplaySuccess] = useState(false);
	const [errorMessage, setErrorMessage] = useState('');

  async function handleSubmit(e: any) {
    e.preventDefault();

		const user: SignUpFormValues = {
      firstName: firstName, 
      lastName: lastName, 
      email: email, 
      dateOfBirth: new Date(dateOfBirth), 
      password: password, 
      confirmPassword: confirmPassword,
      nationality: nationality
    }

		signUp(user).then(response => {
      if (response?.status === 200) {
        setFirstName('');
        setLastName('');
        setEmail('');
        setDateOfBirth(Date());
        setPassword('');
        setConfirmPassword('');
        setNationality('');
  
        setDisplaySuccess(true);
        setDisplayWarning(false);
        setErrorMessage('');
  
        router.navigate('/');
      } else {
        setDisplaySuccess(false);
        setDisplayWarning(true);
        setErrorMessage(response?.data.reason);
      }
    }).catch(error => {
      setDisplaySuccess(false);
      setDisplayWarning(true);
      setErrorMessage(error.data.reason);
    
    })

		
  }
  
  return (
    <div className="container">
          <div className="row justify-content-center">
            <div className="col-xl-5 col-md-8 mt-3">
            {displaySuccess && (
              <div className='alert alert-success' role='alert'>
                User added successfully!
              </div>
            )}
            {displayWarning && (
              <div className='alert alert-danger' role='alert'>
                {errorMessage || 'Something went wrong!'}
              </div>
            )}
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
                  <label className="form-label" htmlFor="nationality">Nationality </label>
                  <input 
                    type="text" 
                    id="nationality" 
                    className="form-control" 
                    required
                    onChange={e => setNationality(e.target.value)}
                    value={nationality}
                    />
                </div>
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="password">Password </label>
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
                  <label className="form-label" htmlFor="confirmPassword">Confirm Password </label>
                  <input 
                    type="password" 
                    id="confirmPassword" 
                    className="form-control" 
                    required
                    onChange={e => setConfirmPassword(e.target.value)}
                    value={confirmPassword}
                    />
                </div>
                <button type="submit" className="btn btn-primary btn-block" onClick={e => handleSubmit(e)}>Sign in</button>
              </form>
            </div>
          </div>
        </div>
  )
})