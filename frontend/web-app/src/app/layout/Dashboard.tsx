import  {  Container} from "react-bootstrap";
import GamesList from "../Components/GamesList";
import { useVideogames } from "../../lib/hooks/useVideogames";


export default function Dashboard() {
  const {videoGames, isPending} = useVideogames()
  if (isPending || !videoGames) {
    return <div className="text-center">Loading..</div>
  }
  return (
    
    <Container>
      <h2 className="text-center">Video Games Catalogue</h2>
      <h3>Video Game Filters To Be Added</h3>
      <GamesList />
      
    </Container>
  )

}
