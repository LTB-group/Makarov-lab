using System;

public class ComputerGame {
    private string name;
    private PegiAgeRating pegiAgeRating;
    private double budgetInMillionsOfDollars;
    private int minimumGpuMemoryInMegabytes;
    private int diskSpaceNeededInGB;
    private int ramNeededInGb;
    private int coresNeeded;
    private double coreSpeedInGhz;

    public ComputerGame(string name, 
                        PegiAgeRating pegiAgeRating,
                        double budgetInMillionsOfDollars,
                        int minimumGpuMemoryInMegabytes,
                        int diskSpaceNeededInGB,
                        int ramNeededInGb,
                        int coresNeeded,
                        double coreSpeedInGhz) {
        this.name = name;
        this.pegiAgeRating = pegiAgeRating;
        this.budgetInMillionsOfDollars = budgetInMillionsOfDollars;
        this.minimumGpuMemoryInMegabytes = minimumGpuMemoryInMegabytes;
        this.diskSpaceNeededInGB = diskSpaceNeededInGB;
        this.ramNeededInGb = ramNeededInGb;
        this.coresNeeded = coresNeeded;
        this.coreSpeedInGhz = coreSpeedInGhz;
    }

    public string getName() {
        return name;
    }

    public PegiAgeRating getPegiAgeRating() {
        return pegiAgeRating;
    }

    public double getBudgetInMillionsOfDollars() {
        return budgetInMillionsOfDollars;
    }

    public int getMinimumGpuMemoryInMegabytes() {
        return minimumGpuMemoryInMegabytes;
    }

    public int getDiskSpaceNeededInGB() {
        return diskSpaceNeededInGB;
    }

    public int getRamNeededInGb() {
        return ramNeededInGb;
    }

    public int getCoresNeeded() {
        return coresNeeded;
    }

    public double getCoreSpeedInGhz() {
        return coreSpeedInGhz;
    }
}

public enum PegiAgeRating {
    P3, P7, P12, P16, P18
}

public class Requirements {
    private int gpuGb;
    private int HDDGb;
    private int RAMGb;
    private double cpuGhz;
    private int coresNum;

    public Requirements(int gpuGb, int HDDGb, int RAMGb, double cpuGhz, int coresNum) {
        this.gpuGb = gpuGb;
        this.HDDGb = HDDGb;
        this.RAMGb = RAMGb;
        this.cpuGhz = cpuGhz;
        this.coresNum = coresNum;
    }

    public int getGpuGb() {
        return gpuGb;
    }

    public int getHDDGb() {
        return HDDGb;
    }

    public int getRAMGb() {
        return RAMGb;
    }

    public double getCpuGhz() {
        return cpuGhz;
    }

    public int getCoresNum() {
        return coresNum;
    }
}

public interface PCGame {
    string getTitle();
    int getPegiAllowedAge();
    bool isTripleAGame();
    Requirements getRequirements();
}

public class ComputerGameAdapter : PCGame {
    private readonly ComputerGame _computerGame;

    public ComputerGameAdapter(ComputerGame computerGame) {
        _computerGame = computerGame;
    }

    public string getTitle() {
        return _computerGame.getName();
    }

    public int getPegiAllowedAge() {
        return int.Parse(_computerGame.getPegiAgeRating().ToString().Substring(1));
    }

    public bool isTripleAGame() {
        return _computerGame.getBudgetInMillionsOfDollars() > 50;
    }

    public Requirements getRequirements() {
        return new Requirements(
            _computerGame.getMinimumGpuMemoryInMegabytes() / 1024,
            _computerGame.getDiskSpaceNeededInGB(),
            _computerGame.getRamNeededInGb(),
            _computerGame.getCoreSpeedInGhz(),
            _computerGame.getCoresNeeded()
        );
    }
}

public class Program {
    public static void Main(string[] args) {
        ComputerGame computerGame = new ComputerGame(
            "Epic Game",
            PegiAgeRating.P16,
            60.0,
            4096, // в мегабайтах
            50,
            8,
            4,
            3.2
        );

        PCGame pcGame = new ComputerGameAdapter(computerGame);

        Console.WriteLine($"Название: {pcGame.getTitle()}");
        Console.WriteLine($"Минимальный возраст: {pcGame.getPegiAllowedAge()}");
        Console.WriteLine($"TripleA: {pcGame.isTripleAGame()}");
        Requirements requirements = pcGame.getRequirements();
        Console.WriteLine($"Требования: GPU {requirements.getGpuGb()} GB, HDD {requirements.getHDDGb()} GB, RAM {requirements.getRAMGb()} GB, CPU {requirements.getCpuGhz()} GHz x {requirements.getCoresNum()}");
    }
}
