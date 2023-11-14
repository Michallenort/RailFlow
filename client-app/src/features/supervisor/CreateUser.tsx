import { observer } from "mobx-react-lite";
import { useState } from "react";
import { router } from "../../app/router/Routes";
import { CreateUserFormValues, SignUpFormValues } from "../../app/models/user";
import agent from "../../app/api/agent";
import { useStore } from "../../app/stores/store";

export default observer(function CreateTrain() {
	const {userStore} = useStore();
	const {createUser} = userStore;

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(Date());
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [nationality, setNationality] = useState('');
	const [role, setRole] = useState('Role');
	const [roleId, setRoleId] = useState(0);

  const [displayWarning, setDisplayWarning] = useState(false);
	const [displaySuccess, setDisplaySuccess] = useState(false);
	const [errorMessage, setErrorMessage] = useState('');

	const roleField = (value: string, id: number) => {
		setRole(value);
		setRoleId(id);
	}

  async function handleSubmit(e: any) {
		e.preventDefault()

		const user: CreateUserFormValues = {
			firstName: firstName, 
      lastName: lastName, 
      email: email, 
      dateOfBirth: new Date(dateOfBirth), 
      password: password, 
      confirmPassword: confirmPassword,
      nationality: nationality,
			roleId: roleId
		}

		await createUser(user).then(response => {
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
			} else {
				setDisplaySuccess(false);
				setDisplayWarning(true);
				setErrorMessage(response?.data.reason);
			}
		}).catch(error => {
			setDisplaySuccess(false);
			setDisplayWarning(true);
			setErrorMessage(error?.data.reason);
		});
	}

  function handleCancel() {
    router.navigate('/supervisor');
  }

  return (
<div className='container mt-5 mb-5'>
			<div className='d-flex justify-content-center'>
				{displaySuccess && (
					<div className='col-md-6 alert alert-success' role='alert'>
						User added successfully!
					</div>
				)}
				{displayWarning && (
					<div className='col-md-6 alert alert-danger' role='alert'>
						{errorMessage || 'Something went wrong!'}
					</div>
				)}
			</div>
			<div className='d-flex justify-content-center '>
				<div className='card col-md-6'>
					<div className='card-header'>Add a new user</div>
					<div className='card-body'>
						<form method='POST'>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='firstName'>First Name</label>
									<input
										type='text'
										className='form-control'
										id='firstName'
										placeholder='First Name'
										value={firstName}
										onChange={e => setFirstName(e.target.value)}
									/>
								</div>
                <div className='row'>
                  <div className='mb-3'>
                    <label htmlFor='lastName'>Second Name</label>
                    <input
                      type='text'
                      className='form-control'
                      id='lastName'
                      placeholder='Last Name'
                      value={lastName}
                      onChange={e => setLastName(e.target.value)}
                    />
                  </div>
                </div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='email'>Email</label>
									<input
										type='email'
										className='form-control'
										id='email'
										placeholder='email'
										value={email}
										onChange={e => setEmail(e.target.value)}
									/>
								</div>
							</div>
              <div className='row'>
								<div className='col-md-6 mb-3'>
									<label htmlFor='dateOfBirth'>Date of birth</label>
									<input
										type='date'
										className='form-control'
										id='dateOfBirth'
										placeholder='Date of birth'
										value={dateOfBirth}
										onChange={e => setDateOfBirth(e.target.value)}
									/>
								</div>
								<div className='col-md-6 mb-3'>
									<label htmlFor="dropdownMenuButton1">Role</label>
									<button
										className='form-control btn btn-secondary dropdown-toggle'
										type='button'
										id='dropdownMenuButton1'
										data-bs-toggle='dropdown'
										aria-expanded='false'>
										{role}
									</button>
									<ul id='roleId' className='dropdown-menu' aria-labelledby='dropdownMenuButton1'>
										<li>
											<a className='dropdown-item' onClick={() => roleField('User', 1)}>
												User
											</a>
										</li>
										<li>
											<a className='dropdown-item' onClick={() => roleField('Employee', 2)}>
												Employee
											</a>
										</li>
										<li>
											<a className='dropdown-item' onClick={() => roleField('Supervisor', 3)}>
												Supervisor
											</a>
										</li>
									</ul>
								</div>
							</div>
              <div className='row'>
								<div className='mb-3'>
									<label htmlFor='nationality'>Nationality</label>
									<input
										type='text'
										className='form-control'
										id='nationality'
										placeholder='Nationality'
										value={nationality}
										onChange={e => setNationality(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='password'>Password</label>
									<input
										type='password'
										className='form-control'
										id='password'
										placeholder='Password'
										value={password}
										onChange={e => setPassword(e.target.value)}
									/>
								</div>
							</div>
              <div className='row'>
								<div className='mb-3'>
									<label htmlFor='confirmPassword'>Confirm password</label>
									<input
										type='password'
										className='form-control'
										id='confirmPassword'
										placeholder='Confirm password'
										value={confirmPassword}
										onChange={e => setConfirmPassword(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<button className='col-md-3 btn btn-primary' onClick={e => handleSubmit(e)}>
										Submit
									</button>
									<button className="col-md-3 mx-2 btn btn-outline-primary" onClick={handleCancel}>
                    Cancel
                  </button>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
  );
})