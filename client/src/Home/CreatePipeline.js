import React, { useState } from 'react';

const PipelineModal = () => {
  const [showModal, setShowModal] = useState(false);
  const [name, setName] = useState('');
  const [pipelineCommands, setPipelineCommands] = useState('');

  const handleNewPipelineClick = () => {
    setShowModal(true);
  };

  const handleNameChange = (e) => {
    setName(e.target.value);
  };

  const handlePipelineCommandsChange = (e) => {
    setPipelineCommands(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Perform pipeline creation logic with name and pipelineCommands
    // You can send the data to an API or handle it as needed
    console.log('Name:', name);
    console.log('Pipeline Commands:', pipelineCommands);
    // Reset the form
    setName('');
    setPipelineCommands('');
    // Close the modal
    setShowModal(false);
  };
  const closeModal=()=>{
    setName("");
    setPipelineCommands("");
    setShowModal(false);
    
  }

  return (
    <div>
      <button onClick={handleNewPipelineClick}>New Pipeline</button>
      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <h2>Create New Pipeline</h2>
            <form onSubmit={handleSubmit}>
              <div>
                <label htmlFor="name">Name:</label>
                <input
                  type="text"
                  id="name"
                  value={name}
                  onChange={handleNameChange}
                />
              </div>
              <div>
                <label htmlFor="pipelineCommands">Pipeline Commands:</label>
                <input
                  type="text"
                  id="pipelineCommands"
                  value={pipelineCommands}
                  onChange={handlePipelineCommandsChange}
                />
              </div>
              <div>
                <button type="submit">Create Pipeline</button>
                <button onClick={closeModal}>Close</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default PipelineModal;