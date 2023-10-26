import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useStore } from "../../app/stores/store";

export default observer(function SignInForm() {

  const { userStore } = useStore();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const onSubmit = (e: any) => {
    e.preventDefault();
    userStore.signIn({email: email, password: password});
  }

  return (
    <div className="container">
          <div className="row justify-content-center">
            <div className="col-xl-5 col-md-8">
              <form className="bg-white rounded shadow-5-strong p-5">
                <div className="form-outline mb-4">
                  <label className="form-label" htmlFor="form1Example1">Email address</label>
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
                  <label className="form-label" htmlFor="form1Example2">Password: </label>
                  <input 
                    type="password" 
                    id="password" 
                    className="form-control" 
                    required
                    onChange={e => setPassword(e.target.value)}
                    value={password}
                    />
                </div>
                <div className="row mb-4">
                  <div className="col d-flex justify-content-center">
                    <div className="form-check">
                      <input className="form-check-input" type="checkbox" value="" id="form1Example3" checked />
                      <label className="form-check-label" htmlFor="form1Example3">
                        Remember me
                      </label>
                    </div>
                  </div>

                  <div className="col text-center">
                    <a href="#!">Forgot password?</a>
                  </div>
                </div>

                <button type="submit" className="btn btn-primary btn-block" onClick={e => onSubmit(e)}>Sign in</button>
              </form>
            </div>
          </div>
        </div>
  )
})