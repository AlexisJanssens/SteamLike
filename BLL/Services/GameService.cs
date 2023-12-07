using BLL.Interface;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using FluentScheduler;

namespace BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IUserRepository _userRepository;

    public GameService(IGameRepository gameRepository, IPriceRepository priceRepository, IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _priceRepository = priceRepository;
        _userRepository = userRepository;
    }
    
    public GameDTO? CreateGame(GameForm form)
    {
        Game? newGame = _gameRepository.Create(form.ToGame());
        if (newGame is null)
        {
            return null;
        }

        Price newPrice = new Price()
        {
            GameId = newGame.GameId,
            PriceId = 0,
            PriceValue = form.Price,
            UpdateDate = DateTime.Now
        };

        _priceRepository.Create(newPrice);

        return newGame.ToGameDTO();



    }

    public IEnumerable<GameDTO> GetAll()
    {
        return _gameRepository.GetAll().Select(x => x.ToGameDTO());
    }

    public Game? GetById(int gameId)
    {
        Game? game = _gameRepository.Get(gameId);
        return game;
    }

    public BuyingRecapDTO? BuyingGame(int gameId, int userId)
    {
        User? user = _userRepository.Get(userId);
        Game? gameToBuy = GetById(gameId);
        Console.WriteLine("0");
        if (user is  null || gameToBuy is null)
        {
            return null;
        }

        Console.WriteLine("1.5");
        if(_gameRepository.GetById(gameId, userId) is not null)
        {
            if (_gameRepository.GetById(gameId, userId)!.Status == 5)
            {
                GameOfGameList updateGame = new GameOfGameList()
                {
                    UserId = userId,
                    GameId = gameId,
                    Date = DateTime.Now,
                    PlayinTime = 0,
                    GiftId = null,
                    Status = 1
                };
                
                double priceUpdate = _priceRepository.Get(gameId, DateTime.Now)!.PriceValue;
                double userWalletUpdate = user.Wallet;

                if (userWalletUpdate < priceUpdate)
                {
                    return null;
                }

                _gameRepository.UpdateGameList(updateGame);
                
                userWalletUpdate -= priceUpdate;
                user.Wallet = userWalletUpdate;

                _userRepository.Update(user);

                BuyingRecapDTO updateRecap = new BuyingRecapDTO()
                {
                    GameName = gameToBuy.Name,
                    Price = priceUpdate,
                    NewWalletAmount = userWalletUpdate,
                    BuyingDate = updateGame.Date,
                    GiftId = null,
                    UserId = userId

                };

                return updateRecap;
                
            }
            return null;
        }            

        Console.WriteLine("1");
        GameOfGameList newGame = new GameOfGameList()
        {
            UserId = userId,
            GameId = gameId,
            Date = DateTime.Now,
            PlayinTime = 0,
            GiftId = null,
            Status = 1
        };

        double price = _priceRepository.Get(gameId, DateTime.Now)!.PriceValue;
        double userWallet = user.Wallet;

        if (userWallet < price)
        {
            return null;
        }

        Console.WriteLine("2");
        _gameRepository.BuyGame(newGame);
        userWallet -= price;
        user.Wallet = userWallet;

        _userRepository.Update(user);

        BuyingRecapDTO recap = new BuyingRecapDTO()
        {
            GameName = gameToBuy.Name,
            Price = price,
            NewWalletAmount = userWallet,
            BuyingDate = newGame.Date,
            GiftId = null,
            UserId = userId

        };

        return recap;
    }
    
    public BuyingRecapDTO? BuyingGame(int gameId, int userId, int receiverId)
    {
        User? user = _userRepository.Get(userId);
        Game? gameToBuy = GetById(gameId);
        Console.WriteLine("0");
        if (user is  null || gameToBuy is null)
        {
            return null;
        } 
        if(_gameRepository.GetById(gameId, receiverId) is not null)
        {
            return null;
        }

        Console.WriteLine("1");
        GameOfGameList newGame = new GameOfGameList()
        {
            UserId = receiverId,
            GameId = gameId,
            Date = DateTime.Now,
            PlayinTime = 0,
            GiftId = userId,
            Status = 2
        };

        double price = _priceRepository.Get(gameId, DateTime.Now)!.PriceValue;
        double userWallet = user.Wallet;

        if (userWallet < price)
        {
            return null;
        }

        Console.WriteLine("2");
        _gameRepository.BuyGame(newGame);
        userWallet -= price;
        user.Wallet = userWallet;

        _userRepository.Update(user);

        BuyingRecapDTO recap = new BuyingRecapDTO()
        {
            GameName = gameToBuy.Name,
            Price = price,
            NewWalletAmount = userWallet,
            BuyingDate = newGame.Date,
            GiftId = userId,
            UserId = receiverId

        };

        return recap;
    }

    public RefundDTO? RefundGame(int userId, int gameId)
    {
        User? user = _userRepository.Get(userId);
        GameOfGameList? gameToRefund = _gameRepository.GetById(gameId, userId);
        if (gameToRefund is null)
        {
            return null;
        } 
        
        if(gameToRefund.PlayinTime > 120 || gameToRefund.GiftId is not null || gameToRefund.Status == 3)
        {
            return null;
        }

        double price = _priceRepository.Get(gameId, gameToRefund.Date)!.PriceValue;
        user!.Wallet += price;
        gameToRefund.Status = 3;
        
        _userRepository.Update(user);
        _gameRepository.UpdateGameList(gameToRefund);

        Game game = _gameRepository.Get(gameId)!;

        RefundDTO recap = new RefundDTO()
        {
            AmountRefund = price,
            GameName = game.Name,
            Message = "Your game has been refund",
            TimePlayed = gameToRefund.PlayinTime
        };

        return recap;
    }

    public IEnumerable<GameOfLibrary> GetMyGames(int userId)
    {
        return _gameRepository.GetMyGames(userId);
    }
    
    public GameDTO? UpdateGame(GameForm form, int gameId)
    {
        Game game = form.ToGame();
        game.GameId = gameId;
        
        if (!_gameRepository.Update(game))
        {
            return null;
        };
        Game updatedGame = _gameRepository.Get(gameId)!;

        // Price newPrice = new Price()
        // {
        //     GameId = updatedGame.GameId,
        //     PriceId = 0,
        //     PriceValue = form.Price,
        //     UpdateDate = DateTime.Now
        // };
        //
        // _priceRepository.Create(newPrice);

        return updatedGame.ToGameDTO();
    }

    public bool UpdatePrice(PriceForm form)
    {
        Price newPrice = new Price()
        {
            GameId = form.GameId,
            PriceId = 0,
            PriceValue = form.NewPrice,
            UpdateDate = DateTime.Now
        };
        
        return _priceRepository.Create(newPrice) is not null;
        
    }

    public IEnumerable<SoldGame> GetSales(int devId)
    {
        return _gameRepository.GetAllSales(devId);
    }
    
    public BuyingRecapDTO? AddWhish(int gameId, int userId)
    {
        User? user = _userRepository.Get(userId);
        Game? gameToWish = GetById(gameId);
        Console.WriteLine("0");
        if (user is  null || gameToWish is null)
        {
            return null;
        }

        Console.WriteLine("1.5");
        if(_gameRepository.GetById(gameId, userId) is not null)
        {
            return null;
        }

        Console.WriteLine("1");
        GameOfGameList newGame = new GameOfGameList()
        {
            UserId = userId,
            GameId = gameId,
            Date = DateTime.Now,
            PlayinTime = 0,
            GiftId = null,
            Status = 5
        };

        if (!_gameRepository.AddWhish(newGame))
        {
            return null;
        };

        BuyingRecapDTO recap = new BuyingRecapDTO()
        {
            GameName = gameToWish.Name,
            Price = 0,
            NewWalletAmount = user.Wallet,
            BuyingDate = newGame.Date,
            GiftId = null,
            UserId = userId

        };

        return recap;
    }

    public bool EnterInGame(int userId, int gameId)
    {
        GameOfGameList? game = _gameRepository.GetById(gameId, userId);
        if (game is null || game.Status != 1)
        {
            return false;
        }
        
        JobManager.Initialize();
        JobManager.AddJob(
            () => _gameRepository.AddGameTime(userId, gameId), s => s.ToRunEvery(1).Minutes()
            );
        
        return _gameRepository.EnterInGame(userId, gameId);
    }

    public bool QuitInGame(int userId, int gameId)
    {
        GameOfGameList? game = _gameRepository.GetById(gameId, userId);
        if (game is null || game.Status != 6)
        {
            return false;
        }
        JobManager.Stop();
        JobManager.RemoveAllJobs();

        return _gameRepository.QuitInGame(userId, gameId);
    }
}