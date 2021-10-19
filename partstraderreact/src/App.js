import {InputBase,Grid} from '@material-ui/core';
import axios from "axios";
import './App.css';
import React, {useEffect,useState} from 'react';
import SearchIcon from '@material-ui/icons/Search';

function App() {
  
  const [compatibleParts, setCompatibleParts] = useState([]);
  const [errorMessage,setErrorMessage] = useState("");
  const [partNumber,setPartNumber]=useState("");

  console.log(partNumber);

      function getCompatibleParts(partNumber) {
        axios.get("https://partstraderapi20211018184223.azurewebsites.net/parts/"+partNumber)
          .then(response => {
            setErrorMessage("");
            setCompatibleParts(response.data);
            console.log(response.data);
        })
        .catch(error => {
          setErrorMessage(error.response.data);
          setCompatibleParts([]);
        });
      }

        const handleSubmit = e => {
          e.preventDefault();

          getCompatibleParts(partNumber);
        }

        function CompatibleParts() 
        {
          if (compatibleParts.length){
          return(
            <div id="CompatibleParts">
            <h2>
              {compatibleParts.length} compatible parts found.
            </h2> 
            <Grid container >
            {compatibleParts.map((part,index)=>{
              console.log(part);
                  return (
                      <Grid item xs={3} key={index}>
                        <div className="GridItem">
                          <h4>{part.partNumber}</h4>
                          <p>{part.description}</p>
                        </div>
                      </Grid>
                  )
                })
              }
              </Grid>
            </div>
          )
          }
          else
          {
            return <div/>
          }
        
      }
        

  return (
    <div className="App">
      <header className="App-header">
      <form onSubmit={handleSubmit}>
        <div className="Search">
          <SearchIcon className="SearchIcon"/>
          <InputBase
              placeholder="...Search Part Number"
              onChange={(e)=>setPartNumber(e.target.value)}/>
          </div>
          <div className="Button" onClick={handleSubmit}>
            Search
          </div>
      </form>
        {
          errorMessage
        }
        <CompatibleParts/>
      </header>
    </div>
  );
}

export default App;
