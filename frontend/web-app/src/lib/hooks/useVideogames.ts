import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";

export const useVideogames = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: videoGames, isPending} = useQuery({
        queryKey: ['videoGames'],
        queryFn: async () => {
            const response = await agent.get<VideoGame[]>('/videogames');
            return response.data;
        }
    });

    const {data: videoGame, isLoading: isLoadingVideogames} = useQuery({
        queryKey: ['videoGames', id],
        queryFn: async () => {
            const response = await agent.get<VideoGame>(`/videogames/${id}`);
            return response.data;
        },
        enabled: !!id
    });

    const updateVideoGame = useMutation({  
        mutationFn: async (game: VideoGame) => {
            await agent.put<VideoGame[]>('/videogames/' + game.id, game);
        },
        onSuccess: async () => {
            // Invalidate the query to refetch the data
            await queryClient.invalidateQueries({ queryKey: ['videoGames'] });
        }   
    });

    const createVideoGame = useMutation({  
        mutationFn: async (game: VideoGame) => {
            const response = await agent.post<VideoGame[]>('/videogames', game);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['videoGames'] });
        }   
    })

    const deleteVideoGame = useMutation({  
        mutationFn: async (id: string) => {
            const response = await agent.delete<boolean[]>(`/videogames/${id}`);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['videoGames'] });
        }   
    })
    return {videoGames, isPending, updateVideoGame, createVideoGame, 
                videoGame, isLoadingVideogames, deleteVideoGame};
}