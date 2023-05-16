import React, { useState } from 'react';

const PipelineModal = () => {
  const [showModal, setShowModal] = useState(false);
  const [name, setName] = useState('');
  const [pipelineCommands, setPipelineCommands] = useState('');
  const [existingCommands, setExistingCommands]=useState([]);
  const [showCommands,setShowCommands]=useState(false);

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
    setExistingCommands([]);
  };
  const closeModal=()=>{
    setName("");
    setPipelineCommands("");
    setShowModal(false);
    setExistingCommands([]);
    
  }
  
  const addCommandHandler = () => {
    if(pipelineCommands.trim()===""){
        return;
    }
    setExistingCommands([...existingCommands, pipelineCommands]);
    setShowCommands(true);
    console.log(existingCommands);
  };
  const deleteLastCommandHandler = () => {
    if(existingCommands.length<2){
        setExistingCommands(existingCommands.slice(0, -1));
        setShowCommands(false);
    }
    else{
        setExistingCommands(existingCommands.slice(0, -1));
    }
    
  };
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
                {showCommands && (<div> <ul>
                    {existingCommands.map((command, index) => (
                      <li key={index}>{command}</li>
                    ))}
                  </ul>
                  <button type='button' onClick={deleteLastCommandHandler}>-</button>
                  </div>)}
                <input
                  type="text"
                  id="pipelineCommands"
                  value={pipelineCommands}
                  onChange={handlePipelineCommandsChange}
                />
                <button type='button' onClick={addCommandHandler}>+</button>
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