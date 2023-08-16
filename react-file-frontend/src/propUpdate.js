import { useLocation} from "react-router-dom";
import { useNavigate } from "react-router-dom";
import React, {useState} from "react";


export default function ProperyUpdate(){

    const { state  } = useLocation();
    const navigate = useNavigate();
    const [ stateUpdated] = useState(state);

    const [inputDataUpdated] = useState(state)
    const [rent, setRent] = useState(inputDataUpdated[0].rent);
    const [deposit, setDeposit] = useState(inputDataUpdated[0].deposit);
    const [longitude, setLongitude] = useState(inputDataUpdated[0].longitude);
    const [latitude, setLatitude] = useState(inputDataUpdated[0].latitude);
    const [postalCode, setPostalCode] = useState(inputDataUpdated[0].postalCode);
    const [costSqm, setCostSqm] = useState(inputDataUpdated[0].costSqm);
    const [title, setTitle] = useState(inputDataUpdated[0].title);
    const [isActive, setIsActive] = useState(inputDataUpdated[0].is_active);


    const [lastData] = useState(state)
    
      
   


    function UpdateInfo(propertyId){
    
        lastData[0].rent = rent
        lastData[0].deposit = deposit
        lastData[0].longitude = longitude
        lastData[0].latitude = latitude
        lastData[0].postalCode = postalCode
        lastData[0].costSqm = costSqm
        lastData[0].title = title
        lastData[0].is_active = isActive
        console.log(lastData[0])
        fetch(`http://localhost:3001/${propertyId}` ,{
            method : 'PUT',
            headers:{
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            body:JSON.stringify(lastData[0])
           
        }).then(()=>{

            navigate("/properties", {state : state}) 
        })
      

      
    }

    
    return(

               
        <div className="propertyPage">
            <section>
                <div className="propertyListUpdated">
                  
                 {stateUpdated.map(({id,city,rent,deposit,longitude,latitude,postalCode,costSqm,title,is_active}) => {
                     if(deposit == null){
                        deposit = 0
                     
                     }
                   
                    return(<div className="propertyListTexts">
                        
                        <p>Id : {id}</p>

                        <p>Rent : <input defaultValue={rent} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setRent( event.target.value)} />

                        </p>
                        <p>deposit : <input defaultValue={deposit} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setDeposit(event.target.value)} />

                        </p>

                        <p>longitude : <input defaultValue={longitude} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setLongitude(event.target.value)} />
                         </p>

                        <p>latitude : <input defaultValue={latitude} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setLatitude(event.target.value)} />
                         </p>

                        <p>Postalcode : <input defaultValue={postalCode} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setPostalCode( event.target.value)} />
                         </p>

                        <p>costSqm : <input defaultValue={costSqm} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setCostSqm(event.target.value)} />
                        </p>

                        <p>Title : <input defaultValue={title} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setTitle( event.target.value)} /></p>


                        <p>Available : <input defaultValue={is_active} type="text"  id="form1" className="form-control" aria-label="text" 
                         onChange={event => setIsActive( event.target.value)} /></p>

                        <p><button className="btnProp" id="buttonProp" onClick={()=> UpdateInfo(id,inputDataUpdated)}>Update</button></p>



                        </div>
                        
                        
                    )}
                    
                    )
                  
                }
                </div>
            </section>
        </div>);





}