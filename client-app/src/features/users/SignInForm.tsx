import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useStore } from "../../app/stores/store";
import { router } from "../../app/router/Routes";

export default observer(function SignInForm() {

  const { userStore } = useStore();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const onSubmit = (e: any) => {
    e.preventDefault();
    userStore.signIn({email: email, password: password});
    router.navigate('/');
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

                <button type="submit" className="btn btn-primary btn-block" onClick={e => onSubmit(e)}>Sign in</button>
              </form>
            </div>
          </div>
        </div>
  )
})