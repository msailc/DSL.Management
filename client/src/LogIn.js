import React from 'react';
import { Link } from 'react-router-dom';
import Register from "./Register.js";
// use app.css for styling
import './App.css';

function LogIn() {
  return (
    <div className="login-container">
      <div className="login-left">
        <h1>Welcome Back!</h1>
        <p>Log in to your account</p>
        <a href="https://github.com/login/oauth/authorize?client_id=<YOUR_CLIENT_ID>&scope=user">
          <img src="https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png" alt="GitHub Logo" />
          <span>Log in with GitHub</span>
        </a>
        <Register/>
      </div>
      <div className="login-right">
        <img src="https://assets.website-files.com/6294d502b5093e3965b91f4d/62ade1aafe6019c151ee7dea_leyre-71SHXwBLp5w-unsplash.png" alt="Login Image" />
      </div>
    </div>
  );
}

export default LogIn;
