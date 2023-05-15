import React, { useState, useEffect } from 'react';

const RegisterForm = () => {
  const [showForm, setShowForm] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [toastMessage, setToastMessage] = useState('');

  const handleRegisterClick = () => {
    setShowForm(true);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (email.trim() === '' || password.trim() === '' || password.length < 4) {
      setToastMessage(' Password needs to be at least 4 characters long.');
      return;
    }

    // Perform registration logic with email and password
    // You can send the data to an API or handle it as needed
    console.log('Email:', email);
    console.log('Password:', password);
    // Reset the form
    setEmail('');
    setPassword('');
  };

  useEffect(() => {
    let toastTimeout;

    if (toastMessage) {
      toastTimeout = setTimeout(() => {
        setToastMessage('');
      }, 5000);
    }

    return () => clearTimeout(toastTimeout);
  }, [toastMessage]);

  return (
    <div>
      <p>New Here?</p>
      <button onClick={handleRegisterClick}>Register</button>
      {showForm && (
        <form onSubmit={handleSubmit}>
            <div>
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={handleEmailChange}
          />
          </div>
          <div>
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={handlePasswordChange}
          />
          </div>
          <button type="submit">Submit</button>
        </form>
      )}
      {toastMessage && (
        <div className="toast">
          <p>{toastMessage}</p>
        </div>
      )}
    </div>
  );
};

export default RegisterForm;