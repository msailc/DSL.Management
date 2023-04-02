import React, { useState } from "react";
import NavBar from "./Navbar";
import Terminal from "./Terminal";
import UserModal from "./UserModal";

function Home() {
  const [modalVisibility, changeModalVisibility] = useState(false);
  function UserButtonHandler() {
    changeModalVisibility(true);
  }
  function CloseModal() {
    changeModalVisibility(false);
  }
  return (
    <>
      <NavBar UserButtonHandler={UserButtonHandler} />
      {modalVisibility && <UserModal CloseModal={CloseModal} />}
      <Terminal />
    </>
  );
}

export default Home;
