import  { Col, Container, Row } from "react-bootstrap";
import GamesList from "../Components/GamesList";
import GameDetail from "../Components/GameDetail";
import VideogameForm from "../features/form/VideogameForm";


type Props = {
    videoGames: VideoGame[];
    selectedGame?: VideoGame ;
    editMode: boolean
    selectGame?: (id: string) => void;
    cancelSelectedGame?: () => void;
    closeForm: () => void;
    openForm: (id?: string) => void;
}

export default function Dashboard({ videoGames, selectedGame, selectGame, cancelSelectedGame, closeForm, editMode, openForm }: Props) {
  

  return (
    
    <Container>
      <h3 className="text-center">Video Games Catalogue</h3>
      <Row>
        <Col xs={12} md={6} lg={4}>
          <GamesList videoGames={videoGames} selectGame={selectGame}/>
        </Col>
        <Col>
          {selectedGame && !editMode && 
          <GameDetail selectedGame={selectedGame} cancelSelectedGame={cancelSelectedGame} openForm={openForm}/>}
          {editMode && <VideogameForm closeForm={closeForm} videoGame={selectedGame}/>}
        </Col>
      </Row>
    </Container>
  )

}
