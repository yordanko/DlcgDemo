import { useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import Dashboard from "./Dashboard";
import NavBar from "./NavBar";
import { useVideogames } from "../../lib/hooks/useVideogames";

function App() {
const [selectedGame, setSelectedGame] = useState<VideoGame | undefined>(undefined);
const [editMode, setEditMode] = useState(false);
const {videoGames, isPending} = useVideogames()




const handleSelectVideogame = (id: string) => {
  setSelectedGame(videoGames!.find(game => game.id === id));
}

const handleCancelSelectedVideogame = () => {
  setSelectedGame(undefined);
}

const handleFormClose = () => {
  setEditMode(false);
}

const handleOpenForm = (id?: string) => {
  if(id) handleSelectVideogame(id);
  else handleCancelSelectedVideogame();
  setEditMode(true);
}


return  (
    <>
      <NavBar/>
      {isPending || !videoGames ? (<div className="text-center">Loading..</div>) : (
        <Dashboard videoGames={videoGames} 
          selectGame={handleSelectVideogame}
          cancelSelectedGame={handleCancelSelectedVideogame}
          selectedGame={selectedGame}
          editMode={editMode}
          openForm={handleOpenForm}
          closeForm={handleFormClose}/>

      )}
      
    </>
  )
  
}

export default App
