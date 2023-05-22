import React, { useState, useEffect } from 'react';
import {useNavigate } from 'react-router-dom';
import axios from 'axios';

import './App.css';

function LogIn() {
  const [showForm, setShowForm] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [toastMessage, setToastMessage] = useState('');
  const [logInEmail, setLogInEmail] = useState('');
  const [logInPassword, setLogInPassword] = useState('');
  const navigate = useNavigate(); // Use the useNavigate hook here

  const showToast = (message) => {
    alert(message);
  };

  const handleRegisterClick = () => {
    setShowForm(true);
  };

  const handleBackClick = () => {
    setShowForm(false);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleLogInSubmit = (e) => {
    e.preventDefault();

    if (logInEmail.trim() === '' || logInPassword.trim() === '') {
      showToast('Invalid user credentials');
      return;
    }

    const data = {
      email: logInEmail,
      password: logInPassword,
    };

    axios
      .post('http://localhost:5017/auth/login', data)
      .then(() => {
        // Login successful
        navigate('/home'); // Redirect to the home page
      })
      .catch(() => {
        // Login failed
        showToast('Invalid user credentials');
      });
  };

  const handleLogInEmailChange = (e) => {
    setLogInEmail(e.target.value);
  };

  const handleLogInPasswordChange = (e) => {
    setLogInPassword(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (email.trim() === '' || password.trim() === '' || password.length < 4) {
      setToastMessage(' Password needs to be at least 4 characters long.');
      return;
    }

    const data = {
      email: email,
      password: password,
    };

    axios
      .post('http://localhost:5017/auth/register', data)
      .then((response) => {
        console.log(response.data); // Handle the response data as needed
      })
      .catch((error) => {
        console.error(error); // Handle any error that occurred
      });

    // Perform registration logic with email and password
    // You can send the data to an API or handle it as needed

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
    <div className="login-container">
      <div className="login-left">
        {!showForm && (
          <div>
            
              <h1>DSL MANAGEMENT</h1>
              <h2>Welcome Back!</h2>
              <form onSubmit={handleLogInSubmit}>
              <h3>Log In to your account</h3>
            
            <div>
              <input
                type="email"
                placeholder="Email"
                value={logInEmail}
                onChange={handleLogInEmailChange}
              />
            </div>
            <div>
              <input
                type="password"
                placeholder="Password"
                value={logInPassword}
                onChange={handleLogInPasswordChange}
              />
            </div>
            <button type="submit">Submit</button>
            <p>New Here?</p>
            </form>
            
            <div>
              <button onClick={handleRegisterClick}>Register</button>
            </div>
          </div>
        )}
        {showForm && (
          <form onSubmit={handleSubmit}>
            <h2>Register Your Account</h2>
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
            <div className='buttons'> 
            <button type="submit">Submit</button>
            <button onClick={handleBackClick}>Back</button>
            </div>
          </form>
        )}
        {toastMessage && (
          <div className="toast">
            <p>{toastMessage}</p>
          </div>
        )}
      </div>
      <div className="login-right">
        <img src="https://external-preview.redd.it/juNisupeo3mP42FY1o2W5bb-b4lTNt36jtk1ygMsuE8.jpg?auto=webp&s=71834ca7b2f3ca216d975cb0e68887267a40ffad" alt="Login Image" />
            {/* <a href="https://github.com/login/oauth/authorize?client_id=<YOUR_CLIENT_ID>&scope=user">
              <img src="https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png" alt="GitHub Logo" />
              <span>Log in with GitHub</span>
            </a> */}
      </div>
    </div>
  );
}

export default LogIn;
